import os
import json
import re
from typing import List
import logging

modules = ["CTAM.Core", "UserRoleModule", "ItemModule", "ItemCabinetModule", "CabinetModule", "CloudAPI", "CommunicationModule", "ReservationModule", "MileageModule"]

debug = False

class File:
    """
    Represents a .cs file
    """

    def __init__(self, base_dir, filename):
        self.base_dir = base_dir
        self.filename = filename
        file_type = None
        if filename.endswith("Command.cs"):
            file_type = "Command"
        elif filename.endswith("Query.cs"):
            file_type = "Query"
        elif filename.endswith("Handler.cs"):
            file_type = "Handler"
        self.file_type = file_type
        self.name = self.get_name()

    def __str__(self):
        prefix = "CTAM_CloudAPI/"
        i = self.base_dir.index(prefix) + len(prefix)
        folder = self.base_dir[i:]
        return f"File<{folder}, {self.filename}>"

    @property
    def path(self):
        return os.path.join(self.base_dir, self.filename)

    def read_file(self) -> List[str]:
        f = open(self.path, mode='r', encoding="utf-8-sig")
        lines = f.readlines()
        f.close()
        return lines

    def write_lines(self, lines):
        f = open(self.path, 'w')
        f.writelines(lines)
        f.close()

    def get_name(self):
        return self.filename \
            .replace('Handler', '') \
            .replace('Command', '') \
            .replace('Query', '') \
            .replace('.cs', '')

    def exists(self):
        pass


class Parser:

    def __init__(self, f: File):
        self.file = f

    def extract_class(self, class_derived_from: str):
        lines = self.file.read_file()
        class_line = next(((i, line) for i, line in enumerate(lines) if class_derived_from in line), None)
        if not class_line:
            raise Exception("Could not find class definition which derives from IRequestHandler")
        class_index, _ = class_line
        open_acc_index = class_index + 1
        open_indent = lines[open_acc_index].index("{")
        close_acc = next(
                ((i, line)
                    for i, line in enumerate(lines[class_index+2:])
                    if line.find("}") == open_indent),
                None)
        if not close_acc:
            raise Exception("Could not find '}' for the end of the class")
        close_acc_index = close_acc[0] + class_index + 2
        if debug:
            lines[open_acc_index] = "x" + lines[open_acc_index][1:]
            lines[close_acc_index] = "x" + lines[close_acc_index][1:]
        return class_index, close_acc_index, lines

    def extract_handler_class(self):
        return self.extract_class(": IRequestHandler")

    def extract_message_class(self):
        return self.extract_class(": IRequest")

    def extract_imports(self):
        lines = [line for line in self.file.read_file() if re.match(r".*using\s[A-Z].*", line)]
        imports = [line[line.index("using")+6:].strip() for line in lines]
        return imports



class ClassComposer:

    def __init__(self):
        pass

    def compose_new_imports(self, message, handler):
        handler_imports = set(Parser(handler).extract_imports())
        message_imports = set(Parser(message).extract_imports())
        new_imports = list(handler_imports.union(message_imports))
        new_imports = sorted(new_imports, key=lambda x: self.comp(x))
        imports = [f"using {imp}" for imp in new_imports]
        return imports

    def comp(self, line):
        if line.startswith("System"):
            return 0
        if line.startswith("Microsoft"):
            return 1
        if any([m in line for m in modules]):
            return 3
        return 2

    def combine_message_with_handler(self, message, handler):
        start_handler, end_handler, handler_lines = Parser(handler).extract_handler_class()
        # Get the range of line numbers of the message class
        start_index, end_index, lines = Parser(message).extract_message_class()
        handler_str = ["\n"]
        # Insert the handler class afer the message class in the message file
        new_lines = lines[:end_index+1] + \
            handler_str + \
            handler_lines[start_handler:end_handler+1] + \
            handler_str + \
            lines[end_index+1:]
        new_lines_without_imports = [line for i, line in enumerate(new_lines) if not line.startswith('using')]
        # Extract the imports from the handler and merge with the imports from the message class
        new_imports = self.compose_new_imports(message, handler)
        new_imports = [imp+"\n" for imp in new_imports]
        # Add new usings to new file
        return new_imports + new_lines_without_imports


sync_modules = [
    #"MileageModule",
    #"ReservationModule",
    #"CommunicationModule",
    "CloudAPI",
    #"ItemCabinetModule",
    #"CabinetModule",
    #"ItemModule",
    #"UserRoleModule",
]
root_dir = os.path.abspath(os.path.join(os.getcwd(), "../"))
print(f"Solution directory: {root_dir}")

def get_files(root):
    fs = []
    for (dirpath, dirnames, filenames) in os.walk(root):
        fs += [File(dirpath, name) for name in filenames if name.endswith('.cs')]
    return fs

errors = []
for module_name in sync_modules:
    print("------------------")
    print(module_name)
    print("------------------")

    module_root = os.path.join(root_dir, module_name)
    print(f"Module directory: {module_root}")

    # Messages(Command and Queries)
    print("- Message")
    message_names = set([])
    ms = []
    for message_type in ['Queries', 'Commands']:
        message_dir = os.path.join(module_root, "ApplicationCore", message_type)
        files = get_files(message_dir)
        message_names = message_names.union(set([f.name for f in files]))
        ms += files
    for i in ms: print(i)

    # Handlers
    print("- Handlers")
    handlers_dir = os.path.join(module_root, "ApplicationCore", "Handlers")
    hs = get_files(handlers_dir)
    handler_names = set([f.name for f in hs])
    for i in hs: print(i.name)

    # Find matches between Message and Handlers
    missing_handlers = message_names.difference(handler_names)
    missing_messages = handler_names.difference(message_names)
    match = len(missing_handlers) == 0 and len(missing_messages) == 0
    if len(missing_handlers) > 0:
        print("-> Messages without matching handlers found")
        for m in missing_handlers:
            print(f"\t- {m}")
    if len(missing_messages) > 0:
        print("-> Handlers without mathing messages found")
        for m in missing_messages:
            print(f"\t- {m}")

    if not match:
        print("--- Stop migration")
        break
    else:
        print(f"--> Messages and handlers validated: {len(handler_names)}")

    # Merge each message in the module with the matching handler
    for message in ms:
        try:
            name = message.name
            matching_handler = next((h for h in hs if h.name == name), None)
            print(f"Name: {name}")
            print(matching_handler)
            lines = ClassComposer().combine_message_with_handler(message, matching_handler)
            message.write_lines(lines)
            # Comment out old Handler class, so Mediatr doesn't use it anymore
            lines = matching_handler.read_file()
            lines = ["/**\n"] + lines + ["*/"]
            matching_handler.write_lines(lines)
        except:
            m = f"Failed to migrate {message.name} in module {module_name}"
            logging.exception(m)
            errors.append(m)

if errors:
    print("---ERRORS---")
    for e in errors:
        print(e)
else:
    print("--- No errors")
