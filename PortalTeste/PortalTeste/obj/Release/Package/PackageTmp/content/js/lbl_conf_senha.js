$("#lbl_conf_senha").css("display", "none");

$("#ipt_senha").keyup(function (event) {
    confirmeSenha();
});

$("#ipt_conf_senha_cli").keyup(function (event) {
    confirmeSenha();
});

function confirmeSenha() {
    var senha = $("#ipt_senha").val();
    var conf_senha = $("#ipt_conf_senha_cli").val();
    

    if (conf_senha.length > 3) {
        if (senha == conf_senha) {
            $("#lbl_conf_senha").text("");
            $("#lbl_conf_senha").css("display", "none");
        }
        else {
            $("#lbl_conf_senha").text("Senhas diferentes.");
            $("span[data-valmsg-for='conf_senha']").css("display", "none");
            $("span[data-valmsg-for='Cli.senha_cli']").css("display", "none");
            $("#lbl_conf_senha").css("display", "block");
        }
    }
}