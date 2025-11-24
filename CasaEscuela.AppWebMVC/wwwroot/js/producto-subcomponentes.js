
    $(function () {
        $("#btnNuevo_Subcomponente").click(function () {
            let tr = $("#tbTrClone_Subcomponente").clone().removeAttr("id").show();
            $("#tbBody_Subcomponentes").append(tr);

            $(tr).find("button[data-eliminartbitem]").click(function () {
                $(tr).remove();
                renombrarIndicesSub();
            });

            $(tr).find(".subproducto-autocomplete").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: urlAutoCompleteProducto,
                        data: { query: request.term },
                        type: "GET",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data, function (item) {
                                return {
                                    label: item.label,
                                    value: item.descripcion,
                                    idProducto: item.id
                                };
                            }));
                        }
                    });
                },
                select: function (event, ui) {
                    var $row = $(this).closest("tr");
                    $row.find("input[data-nameprop='IdProductoHijo']").val(ui.item.idProducto);

                    $row.find(".select-bodega").empty().append('<option value="">Seleccione bodega</option>');
                    $row.find(".select-unidad").empty().append('<option value="">Seleccione unidad</option>');
                    $row.find("input[data-nameprop='Cantidad']").val('');

                    $.ajax({
                        url: urlObtenerBodegasUnidadesProducto,
                        data: { idProducto: ui.item.idProducto },
                        type: 'GET',
                        dataType: 'json',
                        success: function (data) {
                            $row.attr("data-inventarioporbodega", JSON.stringify(data.bodegas));
                            $row.attr("data-unidadesalternativas", JSON.stringify(data.unidades));

                            $.each(data.bodegas, function (i, bodega) {
                                $row.find(".select-bodega").append(
                                    $('<option>', { value: bodega.id, text: bodega.nombre })
                                );
                            });

                            $.each(data.unidades, function (i, unidad) {
                                $row.find(".select-unidad").append(
                                    $('<option>', { value: unidad.id, text: unidad.value || unidad.nombre })
                                );
                            });
                        }
                    });

                    renombrarIndicesSub();
                }
            });

            $(tr).on('change', '.select-bodega', function () {
                actualizarCantidadSub(this);
            });

            renombrarIndicesSub();
        });

    renombrarIndicesSub();

    function renombrarIndicesSub() {
                var nameList = $("#tbBody_Subcomponentes").attr("data-namelist");
    $("#tbBody_Subcomponentes tr:visible").each(function (index) {
        $(this).find("[data-nameprop]").each(function () {
            var nameProp = $(this).attr("data-nameprop");
            var newName = nameList + "[" + index + "]." + nameProp;
            $(this).attr("name", newName);
        });
                });
            }
        });

    function actualizarCantidadSub(selectBodega) {
            var $row = $(selectBodega).closest("tr");
            var idBodega = $(selectBodega).val();

            var inventarioStr = $row.attr("data-inventarioporbodega");
            var inventario = inventarioStr ? JSON.parse(inventarioStr) : [];

                    var bodega = inventario.find(b => b.id == idBodega);
            var cantidad = bodega ? (bodega.cantidadDisponible || 0) : 0;

            $row.find("input[data-nameprop='Cantidad']").val('');
     }

    function eliminarFila_Subcomponente(button) {
        $(button).closest("tr").remove();
        renombrarIndicesSub();
    }
