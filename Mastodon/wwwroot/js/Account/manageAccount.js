$('#subscriptionPlans').change(function () {
    hideAllPaymentButtons();
    hideUpgradeDowngradeText();

    let currentPlan = $('#currentSubscriptionPlan').text();
    let selectedPlan = $('#subscriptionPlans')[0].value;
    if (currentPlan === selectedPlan) {
        return;
    }

    let payId = "pay" + $('#subscriptionPlans')[0].value;
    $("#" + payId).show();

    showUpgradeDowngradeText(currentPlan, selectedPlan);

});

function showUpgradeDowngradeText(currentPlan, selectedPlan) {
    if (currentPlan === "Free") {
        $("#upgradeText").show();
        return;
    }

    if (currentPlan === "Bronze") {
        if (selectedPlan === "Free") {
            $("#downgradeText").show();
            return;
        } else {
            $("#upgradeText").show();
            return;
        }
    }

    if (currentPlan === "Silver") {
        if (selectedPlan === "Bronze" || selectedPlan === "Free") {
            $("#downgradeText").show();
            return;
        } else {
            $("#upgradeText").show();
            return;
        }
    }

    if (currentPlan === "Gold") {
        $("#downgradeText").show();
        return;
    }
}

function hideAllPaymentButtons() {
    $("#payBronze").hide();
    $("#paySilver").hide();
    $("#payGold").hide();
}

function hideUpgradeDowngradeText() {
    $("#upgradeText").hide();
    $("#downgradeText").hide();
}

function cancelSubscription() {
    $("#cancelSubscriptionModal").modal('show');
}

function confirmStopSubscription() {
    axios.get('/Manage/CancelAccountPlan')
        .then(function (response) {
            if (response.data === "Success") {
                window.location = "./Manage"
            }
        })
        .catch(function (error) {
            console.log(error);
        });
}