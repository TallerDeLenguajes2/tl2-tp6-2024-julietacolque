﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function idProducto(id) {
    document.getElementById('idProductoEliminar').value = id;
}


function idPresupuesto(id) {
    document.getElementById('idPresupuesto').value = id;
}


function ObtenerIDs(idP, cantidad) {

    document.getElementById('idProductoModificar').value = idP;
    document.getElementById('cantidad').value = cantidad;
}