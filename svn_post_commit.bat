cd /D "D:\Documents\Programmes\Visual Basic\Visual Studio\Create Synchronicity\trunk\"
set ver=%4
set ver=###%ver%###
set ver=%ver:"###=%
set ver=%ver:###"=%
set ver=%ver:###=%
package.bat %ver% > buildlog.txt
spawn post-commit.exe http://synchronicity.sourceforge.net/code/post-commit.php sql-log.txt createsoftware synchronicity %*