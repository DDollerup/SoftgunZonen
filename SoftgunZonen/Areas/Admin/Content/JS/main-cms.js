function ajaxRegisterPostForm(action, btn, targetForm, swalSuccess) {
    var $form = $(targetForm);
    $form.on("click", btn, function () {
        var formData = $form.serialize();
        $.ajax({
            type: "POST",
            url: action,
            data: formData,
            success: function (response) {
                if (response.status) {
                    closeModal();
                    swal({
                        type: 'success',
                        title: 'Success!',
                        text: swalSuccess
                    });
                    ajaxGetCurrentPage();
                }
                else {
                    swal({
                        type: 'error',
                        title: 'Oops...',
                        text: 'An error occured.'
                    });
                }
            },
            error: function (error) {
                alert(error);
            }
        });
    });

}

function ajaxPostID(action, value) {
    $(document).ready(function () {
        $.ajax({
            type: "POST",
            url: action,
            data: { id: value },
            success: function (response) {
                if (response.status) {
                    swal({
                        type: 'success',
                        title: 'Success!',
                        text: response.msg
                    });
                    ajaxGetCurrentPage();
                }
                else {
                    swal({
                        type: 'error',
                        title: 'Oops...',
                        text: response.msg
                    });
                }
            },
            error: function (error) {
                alert(error);
            }
        });
    });
}

function ajaxGetCurrentPage() {
    $(document).ready(function () {
        $.ajax({
            type: "GET",
            url: window.location.pathname,
            success: function (response) {
                var target = $("#renderBody");
                target.empty();
                target.load(window.location.pathname);
            },
            error: function (error) {
                alert(error);
            }
        })
    });
}

function ajaxGetModal(action, loadTarget = "#modalContainer", emptyFirst = true) {
    $(document).ready(function () {

        $.ajax({
            type: "GET",
            url: action,
            success: function (response) {
                if (emptyFirst) {
                    $(loadTarget).empty();
                }
                var target = $(loadTarget);
                target.load(action, function () {
                    var modalForm = target.find(".modalForm");
                    target.find(".modalForm").fadeIn();
                });

            },
            error: function (error) {
                alert(error);
            }
        })

    });
}

function closeModal(target = ".modalForm") {
    $(target).fadeOut();
}

function simpleSearch(dataInput, dataAttr, clearBtn) {
    var input = $(dataInput).val().toUpperCase();
    $(dataAttr).each(function () {
        var attr = dataAttr.replace('[', '').replace(']', '');
        if (this.getAttribute(attr).toUpperCase().indexOf(input) > -1) {
            this.style.display = "";
        }
        else {
            this.style.display = "none";
        }
    });

    $(clearBtn).click(function () {
        $(dataInput).val('');
        simpleSearch(dataInput, dataAttr, clearBtn);
    });
}

function ajaxConfirmDelete(text, action, value, swalSuccess) {
    swal({
        title: 'Delete?',
        text: text,
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            ajaxPostID(action, value, swalSuccess);
            return true;
        }
        return false;
    });
}

function categoryAdded(selectElement, name, targetContainer) {
    var select = $(selectElement);
    var value = select.val();
    var option = $("#" + select.attr("id") + " option:selected");
    var inputElement = "<tr><td>" + option.text() + "</td><td><a href='#' data-val='" + value + "' class='remove-item'><i class='material-icons'>delete_forever</i></a>" + "<input name='" + name + "' value='" + value + "' type='hidden' />"; + "</td></tr>"

    option.hide();

    var container = $(targetContainer);
    container.append(inputElement);

    container.on("click", ".remove-item", function () {
        $(this).closest("tr").remove();
        $("#" + select.attr("id") + " option[value='" + $(this).attr("data-val") + "']").show();
    });

    select.val('');
}

function imagePreview(input, img) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(img).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}