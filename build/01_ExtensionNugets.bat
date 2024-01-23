cd ../extensions/CookBook.Extensions.Configuration/build
call 01_PackNuget.bat
call 02_PublishNuget.bat
cd ../../../build

cd ../extensions/CookBook.Extensions.AspNetCore/build
call 01_PackNuget.bat
call 02_PublishNuget.bat
cd ../../../build


