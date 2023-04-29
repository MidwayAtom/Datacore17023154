# Data Core
## Installing and setting up the Database
The application uses a `PostgreSQL 12` database to store all of its information. You need the latest version, and should also install pgAdmin from the installer as well. pgAdmin is used to manage Postgre databases.

Once the installer is running, you will be walked through the setup of the local database. Make sure to remember the password you use for it. While you can change it later if you forget, its more difficult to do.

## Configuring the Database
Launch pgAdmin. You may be asked to do some initial setup. Once done, log into your local database. You should see the Mail Server database if you used Postgre for the Mail Server. From there, you can right click on the `Databases` tab on the left, and create a new database. Give it a simple name, and hit save.

The new database will be created. Ensure that under `Schemas` there is a `public` schema. If there isn't one, right click on `Schemas` and hit create, name it `public` and hit save.

## Run it
To make sure everything is running, just start the startup project from Visual Studio (or your code editor).

### Updating the Database
At any point if a change is made to the database objects, a new migration is needed. Every release will have its own migration, that should never be deleted. Deleting a release migration will cause loss of data.

To add a new migration for your changes, go to the Packet Manager Console in Visual Studio. Type `Add-Migration Dev-MyName-MigrationName` where MyName is your name and MigrationName is the name of the migration. If at any point you need to revert this, type `Remove-Migration`. Never remove migrations labeled `Release`.

There should only be a single migration per release, so keep your commits to including at most one additional `Dev` migration. Any new changes should be removed with `Remove-Migration` and a new migration added. As this breaks the database when the app attempts to update it, make sure to `Drop Cascade` the `Schema` and create a new `public` schema whenever you remove and old migration and add a new migration.

### Testing Accounts
The development admin account is `Atom`. Development account password is `atom`.

