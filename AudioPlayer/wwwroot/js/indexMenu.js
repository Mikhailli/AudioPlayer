function buildDataTable()
{
        table = $('#audio_table').DataTable(
        {
            'info': false,
            'lengthMenu': [4, 8, 16],
            'rowReorder': true,
            'order': [],
            "processing": true,
            "serverSide": true,
            "filter": true,
            "ajax": {
                "url": `/Home/get`,
                "type": "POST"
            },
            'columnDefs': [
                { orderable: false, targets: '_all' }
            ],
            "columns": [
                {
                    data: null,
                    name: 'NumberInPlayList',
                    render: function(data)
                    {
                        return `
                            <td> 
                                ` + data.numberInPlayList + `
                            </td>`
                    }
                },
                {
                    data: null,
                    name: 'Name',
                    render: function(data)
                    {
                        return `
                            <td> 
                                ` + data.name + `
                            </td>`
                    }
                },
                {
                    data: null,
                    name: 'Duration',
                    render: function(data)
                    {
                        return`
                          <td>
                            ` + data.duration + `
                          </td>`
                    }
                },
                {
                    data: null,
                    render: function (data, type, row) {
                        return '<button class="btn btn-sm btn-primary mr-1" data-rename-btn ><span class="fas fa-pencil-alt"></span> Изменить</button>' +
                        '<button class="btn btn-sm btn-danger my-1" data-delete-btn data-url="/Home/DeleteAudio?audioId=' + data.numberInPlayList + '"><span class="fas fa-times"></span> Удалить</button>';
                    },
                    width: 'auto'
                }
            ],
            'language':
                {
                    'search': 'Поиск',
                    'lengthMenu': 'Показать _MENU_ элементов',
                    'zeroRecords': 'Совпадений не найдено',
                    'processing': 'Данные загружаются...',
                    'paginate':
                        {
                            'previous': '<',
                            'next': '>'
                        }
                },
            searchPanes: {
                viewTotal: true
            }
        }).page('first').draw('page');
        
        
        var placeholderElement = $('#modal-placeholder');
        
        placeholderElement.on('hide.bs.modal', '.modal', function () {
            placeholderElement.off('click', '[data-dismiss="modal"]');
        });
        
        table.on('click', 'button[data-delete-btn]', function () {
            var request = $(this).data('url');
            $.get(request).done(function (data) {
                placeholderElement.html(data);
                placeholderElement.find('.modal').modal('show');
            });
            
            placeholderElement.on('click',
                '[data-save="modal"]',
                function (event) {
                event.preventDefault();
                
                var form = $(this).parents('.modal').find('form');
                var actionUrl = form.attr('action');
                var dataToSend = form.serialize();
                
                $.post(actionUrl, dataToSend).done(function (data) {
                    var newBody = $('.modal-body', data);
                    placeholderElement.find('.modal-body').replaceWith(newBody);
                    
                    
                        try {
                            table.draw(false);
                            
                        } finally {
                            placeholderElement.off('click', '[data-save="modal"]');
                        }
                    placeholderElement.find('.modal').modal('hide');
                });
                
            });

            placeholderElement.on('click',
                '[data-mdb-dismiss="modal"]',
                function (event) {
                    event.preventDefault();
                    placeholderElement.find('.modal').modal('hide');
                });
        });

   /* playlist = [];
    for (let i = 0; i < count; i++){
        let song = document.getElementsByName('audioName')[i].textContent;
        console.log(song);
        playlist.push(song);
    }*/
}