"use strict";

$('#subscriptionPlans').change(function () {
    hideAllPaymentButtons();
    hideUpgradeDowngradeText();

    let currentPlan = $('#currentSubscriptionPlan').text();
    let selectedPlan = $('#subscriptionPlans')[0].value;
    if (currentPlan === selectedPlan) {
        return;
    }

    let downgradeSubscription = userDowngradingSubscription(currentPlan, selectedPlan);

    if (downgradeSubscription) {
        $("#DowngradeSubscription").show();
    } else {
        //Must pay to upgrade subscription
        let payId = "pay" + $('#subscriptionPlans')[0].value;
        $("#" + payId).show();
    }
});

function userDowngradingSubscription(currentPlan, selectedPlan) {
    if (currentPlan === "Free") {
        $("#upgradeText").show();
        return false;
    }

    if (currentPlan === "Bronze") {
        if (selectedPlan === "Free") {
            $("#downgradeText").show();
            return true;
        } else {
            $("#upgradeText").show();
            return false;
        }
    }

    if (currentPlan === "Silver") {
        if (selectedPlan === "Bronze" || selectedPlan === "Free") {
            $("#downgradeText").show();
            return true;
        } else {
            $("#upgradeText").show();
            return false;
        }
    }
}

function hideAllPaymentButtons() {
    $("#payBronze").hide();
    $("#paySilver").hide();
    $("#DowngradeSubscription").hide();
}

function hideUpgradeDowngradeText() {
    $("#upgradeText").hide();
    $("#downgradeText").hide();
}

function downgradeSubscription() {
    $("#downgradeSubscriptionModal").modal('show');
}

function confirmDowngradeSubscription() {
    axios.get('/Manage/DowngradeSubscription' + '?newPlan=' + $('#subscriptionPlans')[0].value)
        .then(function (response) {
            if (response.data === "Success") {
                window.location = "./Manage"
            }
        })
        .catch(function (error) {
            console.log(error);
        });
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