﻿$(document).ready(function () {
    $('#Numero').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
});