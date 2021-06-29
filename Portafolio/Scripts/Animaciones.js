M.AutoInit();//para que funcione el munu y la foto

$(document).ready(function () {
    //Se usa un selector para captura el id del div que contiene
    //el nombre. Luego se usa una funcion de JQuery para animar el nombre.
    //La funcion div.animate pueded recibir tres parametros
    //parametro1 = una propiedad css modificable.
    //parametro2 = velocida en milisegundos o predeterminada con una frase como slow.
    //parametro3 = una callback (no incluida).
    let name = $("#headerName");
    let description = $("#headerDescription");
    let description1 = $("#headerDescription1");

    name.fadeTo(1000, 1);
    name.animate({ left: '23%' }, "slow");
    name.animate({ fontSize: '60px' }, "slow", function () {
        description.fadeTo(2000, 1);
       
        description1.fadeTo(2000, 1);

    });
});

//Para hacer funcionar el parallax
document.addEventListener('DOMContentLoaded', function () {
    let elems = document.querySelectorAll('.parallax');
    let instances = M.Parallax.init(elems);
});
