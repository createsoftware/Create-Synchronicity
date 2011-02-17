@if "%1" == "/?" goto help

:start
@echo This file is part of Create Synchronicity.
@echo Create Synchronicity is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
@echo Create Synchronicity is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
@echo You should have received a copy of the GNU General Public License along with Create Synchronicity.  If not, see http://www.gnu.org/licenses/.
@echo Created by:   Clément Pit--Claudel.
@echo Web site:     http://synchronicity.sourceforge.net.

@set LOCALROOT=..\..\..\..\..\Sites Web\Sourceforge\Synchronicity
@set WEBPAGES=%LOCALROOT%\pages

Xhtml2Latex.exe "%WEBPAGES%\help.php" "%WEBPAGES%\help.tex" /webroot "http://synchronicity.sourceforge.net/" /localroot "%LOCALROOT%\\" /wrap

copy "%WEBPAGES%\help.tex" "build\Create Synchronicity User Manual.tex"
copy "%WEBPAGES%\help.pdf" "build\Create Synchronicity User Manual.pdf"
"C:\Program Files (x86)\PuTTY\pscp.exe" "build\Create Synchronicity User Manual.pdf" "build\Create Synchronicity User Manual.tex" "createsoftware,synchronicity@web.sourceforge.net:/home/groups/s/sy/synchronicity/htdocs/pages"

@goto end

:help
@echo This script will build the PDF Manual for Create Synchronicity.
@echo Usage: manual.bat

:end