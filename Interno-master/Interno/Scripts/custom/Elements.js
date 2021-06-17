function addRow(iten) {

    var pagina = $("#pagina").val();

    if (pagina == "Transportadoras") {
        const tableBodyEl = $('#itens_tableBody').get(0);//ver diferença para cliente

        const row = `<tr>
                    <td>
                        ${iten.TRANSP_NOME}
                    </td>
                    <td>
                        ${iten.TRANSP_CNPJ} 
                    </td>
                    <td class="last-td">
                        <div class="icon-group">
                            <a class="icon btn rounded-square  pt-0 pb-0 info-icon" href="${urlInfoTransport}/?transp_id=${iten.TRANSP_ID}"> </a>
                            <a class="icon btn rounded-square pt-0 pb-0 edit-icon" href="${urlEditTransport}/${iten.TRANSP_ID}"> </a>
                            <form method="post" action="${urlDeleteTransport}/${iten.TRANSP_ID}">
                                <input class="icon btn rounded-square pt-0 pb-0 delete-button trash-icon" method="post" value="" type="submit" onclick="return confirm('Tem certeza que deseja excluir o item ${iten.TRANSP_NOME}? ');"> </a>
                            </form>

                        </div>
                    </td>
                 </tr>`;

        tableBodyEl.innerHTML += row;
    }
    if (pagina == "Clientes") {
        console.log(pagina);
        const tableBodyEl = $('#itens_tableBody').get(0);//ver diferença para cliente

        const row = `<tr>
                    <td>
                        ${iten.CLIENTE_NOME}
                    </td>
                    <td>
                        ${iten.CLIENTE_CNPJ} 
                    </td>
                    <td class="last-td">
                        <div class="icon-group">
                            <a class="icon btn rounded-square  pt-0 pb-0 info-icon" href="${urlInfoCliente}/?cliente_id=${iten.CLIENTE_ID}"> </a>
                            <a class="icon btn rounded-square pt-0 pb-0 edit-icon" href="${urlEditCliente}/${iten.CLIENTE_ID}"> </a>
                            <form method="post" action="${urlDeleteCliente}/${iten.CLIENTE_ID}">
                                <input class="icon btn rounded-square pt-0 pb-0 delete-button trash-icon" method="post" value="" type="submit" onclick="return confirm('Tem certeza que deseja excluir o item ${iten.CLIENTE_NOME}? ');"> </a>
                            </form>

                        </div>
                    </td>
                 </tr>`;

        tableBodyEl.innerHTML += row;
    }

    
}

function addPreviousButton(currentPage, searchkey, pageSize) {

    const listEl = $('#pagination_list').get(0);

    listEl.innerHTML += `<li class="PagedList-skipToPrevious"><a onclick = "reload(${currentPage - 1}, ${(searchkey != null) ? `'${searchkey}'` : null}, ${pageSize})" rel="prev">«</a></li>`;

}

function addNextButton(currentPage, searchkey, pageSize) {

    const listEl = $('#pagination_list').get(0);

    listEl.innerHTML += `<li class="PagedList-skipToNext"><a onclick = "reload(${currentPage + 1}, ${(searchkey != null) ? `'${searchkey}'` : null}, ${pageSize})" rel="next">»</a></li>`;
}

function addPaginationButton(pageNumber, searchkey, pageSize) {

    const listEl = $('#pagination_list').get(0);

    const line = `<li><a onclick = "reload(${pageNumber}, ${(searchkey != null) ? `'${searchkey}'` : null}, ${pageSize})" >${ pageNumber }</a></li>`;

    listEl.innerHTML += line;
}

function addActivePaginationButton(pageNumber, searchkey, pageSize) {

    const listEl = $('#pagination_list').get(0);

    const line = `<li class="active" onclick="reload(${pageNumber}, ${(searchkey != null) ? `'${searchkey}'` : null}, ${pageSize})"><a>${pageNumber}</a></li>`;

    listEl.innerHTML += line;
}