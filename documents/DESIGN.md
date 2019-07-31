# Virtue Journal - Design
# Back End Planning
## Tech Stack
- .NET Core
- Postgres


## Models
### User
- PK: id
- email
- password
- userName

### Virtue
- PK: id
- colour
- created
- description
- icon
- name

### Entry
- PK: id
- title
- description
- created
- lastEdited
- starred

### Virtue Entry Junction
- PK: virtueId, entryId
- FK: virtueId
- FK: entryId

- difficulty


## API Endpoints /api
### Virtue Endpoints
GET     /virtues - Return all user's virtues
POST    /virtues - Add a virtue
GET     /virtues/:id - Get a single Virtue's details
GET     /virtues/:id/edit - Edit a Virtue
PUT     /virtues/:id - Update a Virtue
DELETE  /virtues/:id - Delete a Virtue

### Entry Endpoints
GET     /entries/:virtueId - Get entries for a virtue
GET     /entires/new - Get form to create Entry
POST    /entries - Add Entry
GET     /entries/:id - Get a single Entry
GET     /entries/:id/edit - Edit entry
PUT     /entries/:id - Update entry
DELETE  /entries/:id - Delete entry

### Auth Endpoints
GET     /auth/register - Get register form
POST    /auth/register - Register user
GET     /auth/login - Get login form
POST    /auth/login - Login user
GET     /auth/logout - Logout user

### User Endpoints
GET         /settings - Get user's settings
POST/PUT    /settings - Update user's settings


------------------------------------------------------
# Front End Planning
## Tech Stack
- React, react-router, redux
- TypeScript
- CSS: BEMIT & ITCSS
- moment


## State
Virtues:
- array of virtues (needed for sidebar)
- current virtue

Entries:
- array of entries (for the virtue)

Auth:
- currentUser
- isAuthenticated

Errors:
- object containing k-v pairs of errors?

Loading:
- isLoading


## Routes & Views
### Virtues & Entries
/                       <Landing />
- view for when not logged in?

/dashboard              <Dashboard />
- list HERO Level, Virtues

/virtues/:id            <VirtuePage />
- display Virtue Info + sorted, paginated entries

/virtues/:id/detail     <VirtueDetail>
- display detailed Virtue Info + statistics?

/virtues/new            <VirtueForm />
- form to add a Virtue
/virtues/:id/edit       <VirtueForm />
- form to edit a Virtue

/entries/:id            <EntryDetail />
- display info about the entry

/entries/new            <EntryForm />
- form to add an entry
/entries/:id/edit       <EntryForm />
- form to edit an Entry

### Auth
/register         <Register />
/login            <Login />
/logout           <Logout />
/settings         <Settings />
- display user information. Change email, password, etc.


## Components
### Navigation
<Header />
<Footer />
<Logo />

### Components
<Button />
<Loader />

### Main UI
<Dashboard />
<Level />
<ExpBar />

<VirtueList />
<Virtue />
  <VirtueIcon />
<VirtueForm />

<EntryList />
<Entry />
  <EntryDetail />

<EntryForm />

<Calendar />
<Icon Picker />

### Auth
<RegisterForm />
<LoginForm />
<Profile />
  <EditProfile />