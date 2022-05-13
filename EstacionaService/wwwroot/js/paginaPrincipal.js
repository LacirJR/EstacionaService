document.getElementById('entradaVeiculos').addEventListener("submit", function (evt) {
    evt.preventDefault();
    CadastrarVeiculo();
    ChamarTabela();


}, true);

function CadastrarVeiculo() {
    //objeto post
    let _data = {
        Placa: document.getElementById('placaEntradaPatio').value,
        Descricao: document.getElementById('descricaoEntradaPatio').value,
        TipoVeiculo: document.getElementById('tipoEntradaPatio').value
    }

    //post
    fetch(urlCadastrarVeiculo, {
        method: "POST",
        body: JSON.stringify(_data),
        headers: { "Content-type": "application/json; charset=UTF-8" }
    })
        .then(response => response.json())
        .then(json => console.log(json))
        .catch(err => console.log(err));

    document.getElementById('placaEntradaPatio').value = '';
    document.getElementById('descricaoEntradaPatio').value = '';
    document.getElementById('tipoEntradaPatio').value = '';
}


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
    });
}


function CriarTabela(obj) {
    var texto = '';
    $(obj).each(function (index, linha) {
        var dataEntrada = new Date(linha.entrada);
        var dia = dataEntrada.getDate();
        var mes = dataEntrada.getMonth();
        var horas = dataEntrada.getHours();
        var minutos = dataEntrada.getMinutes();

        if (dia.toString().length == 1)
            dia = "0" + dia;
        if (mes.toString().length == 1)
            mes = "0" + mes;
        if (horas.toString().length == 1)
            horas = "0" + horas;
        if (minutos.toString().length == 1)
            minutos = "0" + minutos;

        var dataCompleta = dia + '/' + mes + '/' + dataEntrada.getFullYear() + '  ' + horas + ':' + minutos;

        texto += '<div id="cardCreator" class="card border-primary" style="width: 19rem; margin-right: 0.5vw; ">';
        texto += '<div class="card-header">ID: <b>' + linha.id + '</b></div>'
            + '<div class="card-body">'
            + '<p class="card-text">Entrada: <b>' + dataCompleta + '</b></p>'
            + '<p class="card-text">Placa do veículo: <b>' + linha.placa + '</b></p>'
            + '<p class="card-text">Descrição: <b>' + linha.descricao + '</b></p>'
            + '<a href="#" class="btn btn-primary ">Fechar Conta</a>'
            + '</div>'
            + ' <div class="card-footer">' +
            '<small> Tempo no estacionamento: <b><span class="marcador countdown" style="display:inline-block"> ' + linha.entrada + '</span></b> </small >' +
            '</div >';
        texto += '</div>'
        console.log(linha);
    })

    $("#cardContainer").html(texto);

    var itensCountdown = document.getElementsByClassName("countdown");
    for (i = 0, len = itensCountdown.length; i < len; i++) {
        var item = itensCountdown[i].innerText;
        var tempo = new Date(item);
        $(itensCountdown[i]).countdown({ since: tempo, compact: true, format: 'HMS' });
    }
}

