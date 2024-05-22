# CHANGELOG.md

## 1.0.4 (1-12-2022)

Features:

  - added ExternalReferenceID in modify and create item commands
  - added emails for swap and swapback
  - added environment variable to accomadate addition allowed origin 

## 1.0.5 (22-12-2022)

Features:

  - added searching on user e-mail in user history
  - added crons for support without azure

Fix:
  - fixed showing full description of item on delete confirmation box 
  - fixed removing cabinet that is online

## 1.0.6 (12-01-2023)

Features:
  - Added ability to assign unknown swipecard to user on cabinet and sync it to the cloud
  - Added automatic generation of a pincode when logincode is filled
  - Added e-mail notification to inform user about logincode and pincode
  - Added option to generate web login when creating a new user
  - Added resetting of pincode and informing user of reset pincode

Fix:
  - Fixed syncing of position alias from cloud to cabinet

## 1.1.0 (02-02-2023)

Fix:
  - Fixed return of sensitive data to frontend
  - Fixed default value of isStoredInLocker on itemtype creation
  - Fixed login validity on removing last role with access
  - Fixed resetting of only users with valid preconditions
  - Fixed wrong e-mail values for Repair of item
  - Fixed showing events from hardwareapi on cloud

Features:
  - Added logging of changing cabinet status
  
## 1.2.0 (22-02-2023)

Fix:
- Fixed tagtype nullable to prefend auto input
- Fixed  foutcode description instead of code
- Remove pickup from coreData and add migration. remove from CTAMSeeder rolepermission
- Fixed alle mutaties/verwijderen/toevoegen acties en als deze niet gelogd wordt dan log toevoegen.
- Fixed email templates NP.
- Fixed knop 'ophalen' uit LocalUI en CloudUI
- Fixed when creating locker now count to maximum 10 (for new spanish locker, the positions get connected 1/10 and then 2/10 etc.
- Fixed BulkInsert/batchdelete logging 
- Fixed filter on CTAMModule.Cabinet for CTAMSettings for sync and livesync.
- Fixed increased commandtimeout database functions

Features:
- Added more logging
- Added deployment files for openShift
- Feature 10 cabinet setup instead of 5 for both local connection to localcloudapi and to real cloud url (ctam.dev)
- Feature Interval voor Management, Technisch & Operationele logs instellen in plaats van alle 3 met 1 interval
- Feature api for cabinetactions and cabinetlogs 

## 1.3.0 (08-03-2023)

Fix:
- Verwijderen gebruiker/itemtype/role wordt niet op de kast verwijdert
- removed pickup from productiondata

Features:
- Check for PasswordPolicy from CTAMSettings (LOW, MEDIUM, HIGH) in ForgotPasswordCommand
- Include minimal password length in mail link for changing password. Reset password to something matching the password policy. Password regex with special characters.
- Set Create and Update dates hard in CoreData of CabinetModule, CommunicationModule and UserRoleModule
- MailTemplate defect changed. Added migration
- PasswordPolicy as enum. Regex with underscore
- Header X-Frame-Options SAMEORIGIN toegevoegd in Startup.
- Verwijderen gebruiker/itemtype/role wordt niet op de kast verwijdert
- Lock account after 10 bad logins
- Use password policy also for newly created users.
- Import ItemTypes default IsStoredInLocker = true
- maxPageSize uit PaginatedResult, pageSize -1 verwerken in generiek gedeelte.
- MailTemplates met minimal length password, migration

## 1.3.1 (15-03-2023)

Features:
- Het is nu mogelijk om een IBK nummer zelf op te geven bij het aanmaken


## 1.4.0 (29-03-2023)

Features:
- Automatic logout function and shelf life given to cookie. More information under "Logout CTAM Management Application"
- Functionality to disable positions

Changes: 
- Email Templates updated

## 1.4.1 (20-04-2023)

Features:
- Filter toegevoegd aan Items pagina om te filteren op status 
- Aanpassen Blade positie/nummer toegevoegd aan “IBK aanpassen”

Fix:
- [HOTFIX] Fixed import user command overwriting CardCode of User 

## 1.4.2 (11-05-2023)

Features:
- Added IBK status on IBK overview page

Fix:
- Fixed access intervals on cabinet
- Fixed minimal stock warning mail 
- Order of cabinet action history on overview pages of item and user

## 1.4.3 (31-05-2023)

Features:
- Added IBK hardware configtab on IBK details page (alleen bij setting show_ibk_configtab = true in CTAMSetting tabel)

Fix:
- Fixed Item lenen op de kast tot onder de minimale voorraad wordt niet meteen als waarschuwing getoond en ook geen melding "ververs scherm"
- Fixed foutieve error meldingen bij nieuw wachtwoord versturen
- Fixed OnDisconnectedAsync not set bij een httpcontext zonder token

## 1.4.4 (22-06-2023)

Fix:
- Fixed UserInPossessions syncing for scenario where items have been processed in both offline and online cabinets
- Fixed CabinetPosition that can have no door

## 1.5.0 (12-07-2023)

Fix:
- Removed CleanLogsController to mitigate risk of unauthorized API calls
- Improved refreshtoken mechanism to accomadate for security risks of pentest
- Improved syncCriticalData to accept entities which have been deleted in the cloud but are still sent from a cabinet
- Fixed unsafe cookies
- Fixed Unauthenticated access to web application structure 
- Fixed import of itemtypes not working due to SQL query exception
- Improved speed of UserPersonalItem query on user page
- Fixed assigning of personal items to a user if the item is already part of a personal item (i.e. replacementItemId)
- Fixed Multiple login sessions at the same time possible
- Fixed Sessions remain active after logout

## 1.5.1 (28-07-2023)

Fix:
- Fixed syncing of replace flow of cabinets
## 1.6.0 (04-09-2023)
Fix:
- Fixed importing of items without a user bound to them will be set to NOT ACTIVE and items with a user will be set toACTIVE.
- Fixed allowing syncing of UserInPossessions if userInPossession is already known in cloud database.
- Fixed livesync speed and always returning a response to the caller of a REST import request. 

Features:
- Added functionality for CloudAPI to trigger a full sync on a cabinet in case message is too big for SignalR.

## 1.7.0 (05-10-2023)
Fix:
- CabinetPositionStatus UnknownContent en MissingContent worden niet getoond in UI
- Item met grotere afmetingen wordt toegewezen aan een locker waar die niet in past (ItemType & Spec info) (Omruilen)
- De taal van het menu en formaat van datum is afhankelijk van de ingestelde browsertaal
- CloudUI itemtype sleutel geen h/b/d tonen en deur alleen weergeven bij aanpassen positie als positie keycop is

Feature:
- CabinetDoor aanmaken, syncen en statussen uitwerken
- Als item status 'Niet in gebruik' heeft dan mag die als persoonlijke item wel worden toegewezen
- Postgres verwijzingen in alle docker images aanpassen naar postgres:15 specifiek ipv latest tag

## 1.7.1 (13-10-2023)

Feature:
- Updated mailtemplates Oost-Brabant

## 1.7.2 (13-10-2023)

Fix:
- Fixed status initial of item not being set to active on import and setting of personal item

## 1.7.4 (08-11-2023)

Fix:
- Nieuwe synchronisatie flow met queue geintroduceerd die met een variable in te stellen is wat betreft delay tussen cabinets.
- Als CloudAPI opstart alle cabinets in een queue plaatsen en oppakken met delay van 15 seconden in het begin en daarna elke x delay seconden (default 45 seconden).
- Alleen dit doen voor cabinets die niet initieel zijn.
- Synchronisatie volledig verbetert.
- Import versneld en data in stukjes gehakt (chunks van 15000 per keer).
- Mogelijk nu om 60k users en 60k items te importeren.

## 2.0.0 (29-11-2023)
 
Breaking:
- Added new column to CabinetLog and ManagementLog table called parameters
- Added new column to CabinetAction called LogResourcePath
- Changed column names of ManagementLog, CabinetLog from Log to LogResourcePath which now has a reference to the CTAMSharedLibrary language path instead of a hard log in one language.
 
Feature:
- Logs in cloud application are now searchable in different languages.
- All translations (even backend exceptions) except labels of the ui are now found in the CTAMSharedLibrary folder.
 
Changes:
- Language files now get pulled from CTAMSharedLibrary
- Modifying entities now generates a management log dynamically based on the changes in multi language.
- The cloud now supports date time browser based settings but still keeps the format as the user preference.
 
Fix:
- The generated email when user is changed now supports the changes in multi language.

## 2.1.0 (31-01-2024)

Fix:
- Fixed ManagementLog language changes + email language fixed
- Fixed MigrationFile, renamed from: '20230928122725_RenameManagementLogLogColumn' to '20231012074129_RenameManagementLogLogColumn'
- Fixed ImportItemTypeTest 
- Fixed when shutting down CloudAPI-pods only shut down connected cabinets (instead of all cabinets that are offline in the db)

Feature:
- Added Arabic language support
- Added KeyConductor flawhandling
- Added KeyConductor flawhandling mails
- Added docker-compose file to multiple-cabinets folder (for multi-pod CloudAPI)
- Added 'EmergencyUser' to multi-language translations

Changes:
- Updated DevelopmentData
- ImportLogging 

LastMigrationInRelease: 20240108140044_ArabicMailTemplatesx

## 2.2.0 (21-02-2024)

Fix:
- Fixed Dockerfile from CTAMCron fixed CTAMSharedLibrary reference
- Fixed AccesToken when obtaining web/refreshToken
- Fixed correct item status managementlog
 
Changes:
- Updated ImportItemsCommand.cs, items that are imported but are physically inside a cabinet, don't get changed/updated

LastMigrationInRelease: 20240108140044_ArabicMailTemplates

## 2.2.1 (02-04-2024)

Feature:
- Added environment variable Jwt__RefreshTokenValidityInSeconds, defaults to 86400

Fix:
- Fixed password logging of Redis connectionstring, password is now masked.

Changes:
- Updated environment variable AccessTokenValidityInMinutes to AccessTokenValidityInSeconds

LastMigrationInRelease: 20240108140044_ArabicMailTemplates

## 3.0.0 (03-04-2024)

Feature:
- Added ProductionDataBR for borrow and return seed data
- Added environment variable Jwt__RefreshTokenValidityInSeconds, defaults to 86400
- Added logging of access to admin panel by emergency user, i.e. admin panel accessed with adminpin
- Added column IsActive to MailTemplate table for disabling templates
- Added framework for calling external API's based on triggers with authentication
- Added support for KeyConductor 

Changes:
- Changed environment variable Jwt__AccessTokenValidityInMinutes to Jwt__AccessTokenValidityInSeconds, defaults to 60
- Changed selection of cabinet types to only locker, combilocker and keyconductor

Fix:
- Removed HardwareAPI version
- Removed all mentions of CaptureTech in source code

LastMigrationInRelease: 20240403105510_ReplaceCTByNC

## 3.1.0 (25-04-2024)

Feature:
- Added support for SendGrid (mailing)
- Added strategy pattern for email send method and EmailConfigService for fetching email config (adding support fox EmailConfig settings in database, tenant dependand)
- Added support for multiple languages (Swedish, French, German)
- Added new trigger for external API (NP, SWAP)
- Added config-parameter TENANTS (in non azure context)
- Added support for LicenseKeys (requires environmentVariable LicensePublicKey)
- Added 'license' parameter to tenant secret
- Added support for LicenseKeys to 'Roles, Logging in (GetUserByCredentialsQuery) and FullSync(roles/rights)'

Changes:
- Changed TenantService to I(nterface) TenantService(s)
- Updated ProductionDataBR, added MailTemplates and CTAMSettings to seed

Fix:
- Removed redundant integrations from appsettings

LastMigrationInRelease: 20240403214525_AddedSwedishFrenchGermanTranslations

