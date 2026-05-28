(function () {
    function normalizar(texto) {
        return (texto || "")
            .toString()
            .toLowerCase()
            .normalize("NFD")
            .replace(/[\u0300-\u036f]/g, "");
    }

    function crearBuscador(tabla, indice) {
        if (tabla.dataset.searchReady === "true" || tabla.closest(".modal")) {
            return;
        }

        tabla.dataset.searchReady = "true";
        var contenedor = document.createElement("div");
        contenedor.className = "list-toolbar";

        var caja = document.createElement("div");
        caja.className = "search-box";

        var icono = document.createElement("i");
        icono.className = "fas fa-search";
        icono.setAttribute("aria-hidden", "true");

        var input = document.createElement("input");
        input.type = "search";
        input.className = "form-control js-table-search";
        input.placeholder = "Buscar en el listado";
        input.setAttribute("aria-label", "Buscar en el listado " + (indice + 1));

        caja.appendChild(icono);
        caja.appendChild(input);
        contenedor.appendChild(caja);
        tabla.parentNode.insertBefore(contenedor, tabla);

        input.addEventListener("input", function () {
            var termino = normalizar(input.value);
            var filas = tabla.querySelectorAll("tbody tr");

            filas.forEach(function (fila) {
                var texto = normalizar(fila.innerText);
                fila.style.display = texto.indexOf(termino) >= 0 ? "" : "none";
            });
        });
    }

    function crearModalConfirmacion() {
        if (document.getElementById("modalConfirmacionAccion")) {
            return;
        }

        var wrapper = document.createElement("div");
        wrapper.innerHTML =
            '<div class="modal fade confirm-modal" id="modalConfirmacionAccion" tabindex="-1" aria-hidden="true">' +
            '  <div class="modal-dialog modal-dialog-centered">' +
            '    <div class="modal-content">' +
            '      <div class="modal-header">' +
            '        <h5 class="modal-title"><i class="fas fa-triangle-exclamation me-2 text-warning"></i>Confirmar eliminacion</h5>' +
            '        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>' +
            '      </div>' +
            '      <div class="modal-body">' +
            '        <p id="modalConfirmacionMensaje" class="mb-0">Desea continuar?</p>' +
            '      </div>' +
            '      <div class="modal-footer">' +
            '        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>' +
            '        <button type="button" class="btn btn-danger" id="btnConfirmarAccion">Eliminar</button>' +
            '      </div>' +
            '    </div>' +
            '  </div>' +
            '</div>';

        document.body.appendChild(wrapper.firstElementChild);
    }

    window.confirmarEliminacion = function (control, mensaje) {
        if (control.dataset.confirmado === "true") {
            control.dataset.confirmado = "false";
            return true;
        }

        crearModalConfirmacion();
        var modalElement = document.getElementById("modalConfirmacionAccion");
        var mensajeElement = document.getElementById("modalConfirmacionMensaje");
        var confirmar = document.getElementById("btnConfirmarAccion");

        mensajeElement.textContent = mensaje || "Desea eliminar este registro?";
        confirmar.onclick = function () {
            control.dataset.confirmado = "true";
            bootstrap.Modal.getOrCreateInstance(modalElement).hide();
            control.click();
        };

        bootstrap.Modal.getOrCreateInstance(modalElement).show();
        return false;
    };

    document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll(".table").forEach(crearBuscador);
        crearModalConfirmacion();
    });
})();
