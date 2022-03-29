; (function () {
    `use strict`

    $(document).ready(() => {
        controller.initialize();
    });

    const api = {
        addClient: function (client, onSuccess, onError, onComplete) {
            return this.sendRequest(`post`, `/Home/AddClient`, client, onSuccess, onError, onComplete);
        }, 

        deleteClient: function (clientId, onSuccess, onError, onComplete) {
            return this.sendRequest(`post`, `/Home/DeleteClient`, { clientId }, onSuccess, onError, onComplete);
        },

        editClient: function (client, onSuccess, onError, onComplete) {
            return this.sendRequest(`post`, `/Home/EditClient`, client, onSuccess, onError, onComplete);
        },

        getClients: function (page, onSuccess, onError, onComplete) {
            return this.sendRequest(`get`, `/Home/GetClients`, { page }, onSuccess, onError, onComplete);
        },

        sendRequest: function (method, action, data, onSuccess, onError, onComplete) {
            method = method.toLowerCase();

            const
                onSuccessIsFunction = typeof onSuccess === `function`,
                onErrorIsFunction = typeof onError === `function`,
                onCompleteIsFunction = typeof onComplete === `function`;

            return $.ajax({
                data: data,
                dataType: `json`,
                method: method,
                url: action,

                complete: function (json) {
                    if (onCompleteIsFunction) {
                        onComplete(json);
                    }
                },

                error: function (json) {
                    if (onErrorIsFunction) {
                        onError(json);
                    }
                },

                success: function (json) {
                    if (json.message && onErrorIsFunction) {
                        onError(json);

                        return;
                    }

                    if (onSuccessIsFunction) {
                        onSuccess(json);
                    }
                },
            });
        }
    }

    const controller = {
        clients: null,
        pageInfo: null,


        addClient: function () {
            const
                client = {},
                jClient = $(`#client`);

            jClient.find(`#title`).text(`Add client`);

            const
                jLastName = jClient.find(`#last-name`).closest(`.input-group`).find(`input`)
                    .change(event => client.LastName = event.target.value)
                    .val(``),
                jFirstName = jClient.find(`#first-name`).closest(`.input-group`).find(`input`)
                    .change(event => client.FirstName = event.target.value)
                    .val(``);


            const jSaveButton = jClient.find(`.modal-footer`).find(`button`)
                .click(() => {
                    api.addClient(client, () => {
                        jLastName.unbind();
                        jFirstName.unbind();
                        jSaveButton.unbind();

                        jClient.modal(`hide`);

                        this.initialize(this.pageInfo.TotalPages + 1);
                    }, message => console.log(message));
                });
        },

        createClientsTable: function () {
            const
                clients = this.clients,
                pageInfo = this.pageInfo,
                jTable = $(`#clients`).find(`tbody`).html(``);

            for (const client of clients) {

                const jEdit = $(`<button class="btn btn-primary" data-bs-target="#client" data-bs-toggle="modal">`)
                    .click(() => this.editClient(client))
                    .text(`Edit`);

                const jDelete = $(`<button class="btn btn-danger">`)
                    .click(() => api.deleteClient(client.Id, () => this.initialize(pageInfo.Page), message => console.log(message)))
                    .text(`Delete`);

                const jClient = $(`<tr>`)
                    .append(`<td>${client.LastName}</td>`)
                    .append(`<td>${client.FirstName}</td>`)
                    .append($(`<td>`).append(jEdit))
                    .append($(`<td>`).append(jDelete));

                jTable.append(jClient);
            }
        },

        createClientPagination: function () {
            const
                pageInfo = this.pageInfo,
                jPagination = $(`#clients`).find(`ul`).html(``);

            const jPrevious = $(`<a class="page-link" href="#">`)
                .click(() => this.initialize(pageInfo.Page - 1))
                .text(`Previous`);

            jPagination.append($(`<li class="page-item">`).append(jPrevious));

            for (let page = 1; page <= pageInfo.TotalPages; page++) {
                const jNumber = $(`<a class="page-link" href="#">`)
                    .click(() => this.initialize(page))
                    .text(`${page}`);

                if (page == pageInfo.Page) {
                    jNumber.addClass(`bg-primary text-white`);
                }

                jPagination.append($(`<li class="page-item">`).append(jNumber));
            }

            const jNext = $(`<a class="page-link" href="#">`)
                .click(() => this.initialize(pageInfo.Page + 1))
                .text(`Next`);
            jPagination.append($(`<li class="page-item">`).append(jNext));
        },

        editClient: function (client) {
            const jClient = $(`#client`);

            jClient.find(`#title`).text(`Edit client #${client.Id}`);

            const
                jLastName = jClient.find(`#last-name`).closest(`.input-group`).find(`input`)
                    .change(event => client.LastName = event.target.value)
                    .val(client.LastName),
                jFirstName = jClient.find(`#first-name`).closest(`.input-group`).find(`input`)
                    .change(event => client.FirstName = event.target.value)
                    .val(client.FirstName);


            const jSaveButton = jClient.find(`.modal-footer`).find(`button`)
                .click(() => {
                    api.editClient(client, () => {
                        jLastName.unbind();
                        jFirstName.unbind();
                        jSaveButton.unbind();

                        jClient.modal(`hide`);

                        this.initialize(this.pageInfo.Page);
                    }, message => console.log(message));
                });
        },

        initialize: function (page) {
            api.getClients(page, responseData => {
                const
                    clients = responseData.clients,
                    pageInfo = responseData.pageInfo;

                this.clients = clients;
                this.pageInfo = pageInfo;

                this.createClientsTable();
                this.createClientPagination();

                const jAddButton = $(`#clients`).find(`#add-client`)
                    .click(() => this.addClient());
                
            }, message => console.log(message))
        },
    }
})();