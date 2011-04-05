{
	if ($0 ~ /#SVN-REV/)
		print (gensub(/([0-9]+)\.([0-9]+)\.([0-9]+)\.([0-9]+)/, "\\1.\\2.$WCREV$.\\4", 1));
	else {
		if ($0 ~ /#SVN-DATE/)
			print (gensub(/([0-9]+)\.([0-9]+)\.([0-9]+)\.([0-9]+)/, strftime("\\1.\\2.%Y%m%d.$WCREV$", systime()), 1));
		else
			print $0;
	}
}