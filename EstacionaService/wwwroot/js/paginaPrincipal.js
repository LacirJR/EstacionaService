
ChamarTabela()
function ChamarTabela() {
    $.ajax({
        method: "GET",
        url: urlListaClientes,
        dataType: "json"
    }).done(function (resposta) {
        console.log(resposta);
        CriarTabela(resposta);
    }).fail(function (details, error) {
        console.log(details);
        console.log(error);
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Oops...',
            text: 'Os registros não puderam ser carregados!'
        });
    });
}


function CriarTabela(obj) {
    var texto = "";

    if (obj.length = 0) {
        texto += '<h1>Nenhum veículo no pátio.</h1>'
    } else {

        $(obj).each(function (index, linha) {
            var dataEntrada = new Date(linha.entrada);
            var dia = dataEntrada.getDate();
            var mes = dataEntrada.getMonth();
            if (dataEntrada.getDate().length = 1)
                dia = "0" + dataEntrada.getDate();

            if (dataEntrada.getMonth().length = 1)
                mes = "0" + dataEntrada.getMonth();

            var dataCompleta = dia + '/' + mes + '/' + dataEntrada.getFullYear() + '  ' + dataEntrada.getHours() + ':' + dataEntrada.getMinutes();

            texto += '<div class="card-header">ID: <b>' + linha.nome + '</b></div>'
                + '<div class="card-body">'
                + '<p class="card-text">Entrada: <b>' + dataCompleta + '</b></p>'
                + '<p class="card-text">Placa do veículo: <b>' + linha.placa + '</b></p>'
                + '<p class="card-text">Descrição: <b>' + linha.tipoVeiculo + '</b></p>'
                + '<a href="#" class="btn btn-primary ">Fechar Conta</a>'
                + '</div>'
                + ' <div class="card-footer">' +
                '<small> Tempo no estacionamento: <b><span class="marcador countdown" style="display:inline-block"> ' + linha.entrada + '</span></b> </small >' +
                '</div >';
            console.log(linha);
        })
    }

    $("#cardCreator").html(texto);

    var itensCountdown = document.getElementsByClassName("countdown");
    for (i = 0, len = itensCountdown.length; i < len; i++) {
        var item = itensCountdown[i].innerText;
        var tempo = new Date(item);
        $(itensCountdown[i]).countdown({ since: tempo, compact: true, format: 'HMS' });
    }
}

