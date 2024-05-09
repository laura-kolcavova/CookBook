cd ../extensions/CookBook.Extensions.CSharpExtended/deploy
call 01_Nuget_Pack.bat
call 02_Nuget_Release.bat
cd ../../../deploy

cd ../extensions/CookBook.Extensions.Configuration/deploy
call 01_Nuget_Pack.bat
call 02_Nuget_Release.bat
cd ../../../deploy

cd ../extensions/CookBook.Extensions.AspNetCore/deploy
call 01_Nuget_Pack.bat
call 02_Nuget_Release.bat
cd ../../../deploy


