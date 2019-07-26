var funcController = function () {
    this.initialize = function () {
        loadParents();
        loadData();
        registerEvents();
        registerControls();
    }

    function registerEvents() {
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtName: { required: true },
                txtURL: { required: true },
            }
        });
        //todo: binding events to controls
        $('#ddlShowPage').on('change', function () {
            lg.configs.pageSize = $(this).val();
            lg.configs.pageIndex = 1;
            loadData(true);
        });

        $('#btnSearch').on('click', function () {
            loadData();
        });

        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                loadData();
            }
        });

        $("#btnCreate").on('click', function () {
            resetFormMaintainance();
            initTreeDropDownParent();
            $('#modal-add-edit').modal('show');

        });

        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            loadDetails(that);
        });

        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            deleteFunction(that);
        });

        $('#btnSave').on('click', function (e) {
            saveFunction(e);
        });

    }

    function registerControls() {
        //Fix: cannot click on element ck in modal
        $.fn.modal.Constructor.prototype.enforceFocus = function () {
            $(document)
                .off('focusin.bs.modal') // guard against infinite focus loop
                .on('focusin.bs.modal', $.proxy(function (e) {
                    if (
                        this.$element[0] !== e.target && !this.$element.has(e.target).length
                        // CKEditor compatibility fix start.
                        && !$(e.target).closest('.cke_dialog, .cke').length
                        // CKEditor compatibility fix end.
                    ) {
                        this.$element.trigger('focus');
                    }
                }, this));
        };

    }

    function saveFunction(e) {
        if ($('#frmMaintainance').valid()) {
            e.preventDefault();
            var id = $('#hidIdM').val();
            var name = $('#txtName').val();
            var url = $('#txtURL').val();
            var parentId = $('#ddlParent').combotree('getValue');
            var icon = $('#txtIconCSS').val();
            var sortOrder = $('#txtSortOrder').val();
            var status = $('#ckStatusM').prop('checked') == true ? 1 : 0;
            $.ajax({
                type: "POST",
                url: "/Admin/Function/SaveEntity",
                data: {
                    Id: id,
                    Name: name,
                    ParentId: parentId,
                    URL: url,
                    IconCss: icon,
                    SortOrder: sortOrder,
                    Status: status,
                },
                dataType: "json",
                beforeSend: function () {
                    lg.startLoading();
                },
                success: function (response) {
                    lg.notify('Update function successful', 'success');
                    $('#modal-add-edit').modal('hide');
                    resetFormMaintainance();

                    lg.stopLoading();
                    loadData(true);
                },
                error: function () {
                    lg.notify('Has an error in save function progress', 'error');
                    lg.stopLoading();
                }
            });
            return false;
        }
    }

    function deleteFunction(id) {
        lg.confirm('Are you sure to delete?', function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Function/Delete",
                data: { id: that },
                dataType: "json",
                beforeSend: function () {
                    lg.startLoading();
                },
                success: function (response) {
                    lg.notify('Delete successful', 'success');
                    lg.stopLoading();
                    loadData();
                },
                error: function (status) {
                    lg.notify('Has an error in delete progress', 'error');
                    lg.stopLoading();
                }
            });
        });
    }

    function loadDetails(that) {
        $.ajax({
            type: "GET",
            url: "/Admin/Function/GetById",
            data: { id: that },
            dataType: "json",
            beforeSend: function () {
                lg.startLoading();
            },
            success: function (response) {
                var data = response;
                $('#hidIdM').val(data.Id);
                $('#txtName').val(data.Name);
                initTreeDropDownParent(data.ParentId);
                $('#txtURL').val(data.URL);
                $('#txtIconCSS').val(data.IconCss);
                $('#txtSortOrder').val(data.SortOrder);
                $('#ckStatusM').prop('checked', data.Status == 1);

                $('#modal-add-edit').modal('show');
                lg.stopLoading();

            },
            error: function (status) {
                lg.notify('Có lỗi xảy ra', 'error');
                lg.stopLoading();
            }
        });
    }

    function initTreeDropDownParent(selectedId) {
        $.ajax({
            url: "/Admin/Function/GetParent",
            type: 'GET',
            dataType: 'json',
            async: false,
            success: function (response) {
                var data = [];
                $.each(response, function (i, item) {
                    data.push({
                        id: item.Id,
                        text: item.Name,
                        parentId: item.ParentId,
                        sortOrder: item.SortOrder
                    });
                });
                var arr = lg.unflattern(data);
                $('#ddlParent').combotree({
                    data: arr
                });

                if (selectedId != undefined) {
                    $('#ddlCategoryIdM').combotree('setValue', selectedId);
                }
            }
        });
    }

    function resetFormMaintainance() {
        $('#hidIdM').val(0);
        $('#txtName').val('');
        initTreeDropDownParent('');

        $('#txtURL').val('');
        $('#txtIconCSS').val('');

        $('#txtSortOrder').val('');
        $('#ckStatusM').prop('checked', true);
    }

    function loadParents() {
        $.ajax({
            type: 'GET',
            url: '/admin/function/GetParents',
            dataType: 'json',
            success: function (response) {
                var render = "<option value=''>--Select parent--</option>";
                $.each(response, function (i, item) {
                    render += "<option value='" + item.Id + "'>" + item.Name + "</option>"
                });
            },
            error: function (status) {
                console.log(status);
                lg.notify('Cannot loading parent data', 'error');
            }
        });
    }

    function loadData(isPageChanged) {
        var template = $('#table-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                keyword: $('#txtKeyword').val(),
                page: lg.configs.pageIndex,
                pageSize: lg.configs.pageSize
            },
            url: '/admin/function/GetAllPaging',
            dataType: 'json',
            success: function (response) {
                console.log(response);
                $.each(response.Results, function (i, item) {
                    render += Mustache.render(template, {
                        Id: item.Id,
                        Name: item.Name,
                        URL: item.URL,
                        Parent: item.ParentId,
                        SortOrder: item.SortOrder,
                        Status: lg.getStatus(item.Status)
                    });
                    
                });
                $('#lblTotalRecords').text(response.RowCount);
                if (render != '') {
                    $('#tbl-content').html(render);
                }
                wrapPaging(response.RowCount, function () {
                    loadData();
                }, isPageChanged);
            },
            error: function (status) {
                console.log(status);
                lg.notify('Cannot loading data', 'error');
            }
        });
    }

    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / lg.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                lg.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
}