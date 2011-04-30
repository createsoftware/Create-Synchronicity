xcopy /Y /E "..\Create Synchronicity\bin\Release" "bin"
cd vlc
..\bin\git\bin\git pull
cd ..\linux-2.6
..\bin\git\bin\git pull
cd ..
