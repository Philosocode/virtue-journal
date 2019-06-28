# Virtue Journal - Requirements
## General Planning
### Purpose
Track progress for developing certain traits ("virtues") with a gamified spin.

### Who will use the app?
People who want to:
- become better human begins
- develop their character
- have a "gamer disposition"

### How will the app work?
Platform: web.
Users gain exp and level up their virtues by adding journal entries.



## Stories
I can...

### General
- cool loader
- view privacy policy

### Virtues
- virtue CRUD
  + can edit Virtue colour + icon
- sort virtues by:
  + title (A-Z, Z-A)
  + level (asc, dsc)
- statistics:
  + most recent completion
  + amount of times done this month
  + average per month
  + amount of times done this year

### Entries
- entry list shows condensed entries
- entry CRUD
  + delete: show confirmation message
- search for entry
- sort entries by:
  + most recent / oldest
  + title (A-Z, Z-A)
  + difficulty (easy -> hard, hard -> easy)
- entry pagination or infinity scroll

### Entry
- add an entry to a virtue
  + add an entry to / "tag" multiple virtues
- view a single entry

### Gamify
- view Virtue level + current exp
  + show an EXP bar
- view Total Level (across all virtues)
  + display on main dashboard

### Auth
- create/delete an account
- login/logout
- change password

### Data
- import/export data
- wipe all data and start anew
- backup data daily

### Statistics
- virtue with most entries per week/month/year
- virtue completion over a week/month
- strongest/weakest



## Future
### Ranks/Badges/Achievements
- I can earn ranks/achievements
  + Virtue levels
  + Consistency
- I can view my ranks/achievements