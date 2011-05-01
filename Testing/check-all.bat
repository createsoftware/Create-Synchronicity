@echo off
REM Test profiles use relative paths, and thus must be called from the right folder.

echo ** Synchronizing **
cd bin
"Create Synchronicity.exe" /quiet /run "Fedora 15|Linux|Subversion|Vlc"
cd ..
echo ** Checking results **
call check Fedora-15-Beta-x86_64-DVD
call check subversion
call check linux-2.6
call check vlc