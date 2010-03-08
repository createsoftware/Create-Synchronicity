REM This file is part of Create Synchronicity.
REM 
REM Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
REM Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
REM You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see <http://www.gnu.org/licenses/>.
REM Created by:	Clément Pit--Claudel.
REM Web site:		http://synchronicity.sourceforge.net.

echo %1 %2 %3 %4 %5
"C:\Program Files\NSIS\makensis.exe" "Create Synchronicity\setup_script.nsi"
move Create_Synchronicity_Setup.exe Create_Synchronicity_Setup-r%4.exe
cd "Create Synchronicity\bin\Release"
"C:\Program Files\7-Zip\7z.exe" a "..\..\..\Create_Synchronicity-r%4.zip" "Create Synchronicity.exe" "COPYING" "languages\*"