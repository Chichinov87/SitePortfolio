
	

/*по наведению на иконки соц.сетей, картинки меняются на цветные*/
$(document).on('mouseenter', '.linkIcon img', function(){
    var img = $(this).attr('src');
    $(this).attr('src', img.slice(0,img.length-4) + '2.png');
});


/*при выходе из ссылки на иконки соц.сетей, картинки меняются на черно-белые*/
$(document).on('mouseout', '.linkIcon img', function(){
    var img = $(this).attr('src');
    $(this).attr('src', img.slice(0,img.length-5) + '.png');
});