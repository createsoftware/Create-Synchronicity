REM Test profiles use relative paths, and thus must be called from the right folder.
cd bin
"Create Synchronicity.exe" /quiet /run "Fedora 15|Linux|Subversion|Vlc"
cd ..
check Fedora-15-Beta-x86_64-DVD
check subversion
check linux-2.6
check vlc