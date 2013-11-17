WoWHeadParser will extract quest titles from WoWHead and creates the file 'questname.csv' in the root directory
which needs to be embedded into the libnc project.

To start the process enter:
/start [startquestid]

The tool will then attempt to load quest titles for german and english localization.

e.g.
'/start 0' will result in:
# Loading info for quest: 0
# Loading info for quest: 1
# Loading info for quest: 2
# Loading info for quest: 3
...

Please note that the tool will quit automatically after 50 consecutive failed attempts.
To force abortion either enter: '/stop' or press 'CTRL + C'