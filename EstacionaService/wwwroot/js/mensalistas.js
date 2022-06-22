$(document).ready(function () {
    var table = $('#mensalistas').DataTable({
      ajax: urlDataTable ,
      "columns": [
        { "data": "cpf" },
        { "data": "nome" },
        { "data": "placa" },
        { "data": "tipoVeiculo" },
        { "data": "descricao" },
        { "data": "situacao" },
        { "data": "action" }
      ],
      "processing": true,
      "ordering": false,
      language: {
        url: "//cdn.datatables.net/plug-ins/1.12.1/i18n/pt-BR.json"
      },
      initComplete: function () {
        
        var row = document.getElementsByTagName('tr');
        for (var i = 1; row.length > i; i++) {
          row[i].classList.add("center")
        }
        var cells = document.getElementsByTagName('td');
        for (var i = 0; cells.length > i; i++) {
          cells[i].style.verticalAlign = 'middle'

        }

      stateChange();
      }

    });




  });
  
  function stateChange() {
    setTimeout(function () {
      var searchInput = document.getElementsByClassName('dataTables_filter')[0]
      searchInput.children[0].firstChild.remove()
      searchInput.children[0].children[0].children[0].placeholder = 'Pesquisar'

        
    }, 0);
  }

