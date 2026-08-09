// Utilidades mínimas de navegación para la UI de SAV.
// Se sirve desde el propio origen, por lo que cumple la política de contenido
// sin necesidad de 'unsafe-eval'.
window.sav = {
    // Vuelve a la página anterior. Si no hay historial previo —por ejemplo, si
    // el usuario llegó pegando la URL— devuelve false para que el componente
    // decida el destino alternativo.
    volverAtras: function () {
        if (window.history.length > 1) {
            window.history.back();
            return true;
        }
        return false;
    }
};
