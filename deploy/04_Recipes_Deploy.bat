cd ..\CookBook.Recipes\deploy
call 01_Database_Build.bat
call 02_Database_Release.bat
call 03_Container_Build.bat
call 04_Container_Release.bat
