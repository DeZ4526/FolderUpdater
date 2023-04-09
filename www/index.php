
<?
$dir = './Test/';
GetFiles($dir);

function GetFiles($dir)
{
	if($handle = opendir($dir))
	{
		while(false !== ($file = readdir($handle))) 
		{
			if($file != "." && $file != "..")
				if(is_file($dir . '' .$file))
					$str .= $dir . '' . $file . '|' 
					. md5_file($dir . '' .$file) . '>';
				else GetFiles($dir . '' .$file . '/');
		}
	}
	echo $str;
	
}
?>