<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="UTF-8">
    <title>Free Trial</title>
    <link href="css/form.css" rel="stylesheet" type="text/css">
    <script src="js/validation.js"></script>
</head>
<body class="zf-backgroundBg">
    <!-- Change or deletion of the name attributes in the input tag will lead to empty values on record submission-->
    <div class="zf-templateWidth">
        <form action='https://forms.zohopublic.com/boyhi17/form/FreeTrial/formperma/pvMLyMYEqEiKB18c1W-EquLO5002DH015izVuveZLaU/htmlRecords/submit' name='form' method='POST' onsubmit='javascript:document.charset="UTF-8"; return zf_ValidateAndSubmit();' accept-charset='UTF-8' enctype='multipart/form-data' id='form'>
            <input type="hidden" name="zf_referrer_name" value=""><!-- To Track referrals , place the referrer name within the " " in the above hidden input field -->
            <input type="hidden" name="zf_redirect_url" value=""><!-- To redirect to a specific page after record submission , place the respective url within the " " in the above hidden input field -->
            <input type="hidden" name="zc_gad" value=""><!-- If GCLID is enabled in Zoho CRM Integration, click details of AdWords Ads will be pushed to Zoho CRM -->
            <div class="zf-templateWrapper">
                <!---------template Header Starts Here---------->
                <ul class="zf-tempHeadBdr">
                    <li class="zf-tempHeadContBdr">
                        <h2 class="zf-frmTitle"><em>Free Trial</em></h2>
                        <p class="zf-frmDesc"></p>
                        <div class="zf-clearBoth"></div>
                    </li>
                </ul>
                <!---------template Header Ends Here---------->
                <!---------template Container Starts Here---------->
                <div class="zf-subContWrap zf-topAlign">
                    <ul>
                        <!---------Radio Starts Here---------->
                        <li class="zf-radio zf-tempFrmWrapper zf-twoColumns">
                            <label class="zf-labelName">
                                Radio
                            </label>
                            <div class="zf-tempContDiv">
                                <div class="zf-overflow">
                                    <span class="zf-multiAttType">
                                        <input class="zf-radioBtnType" type="radio" id="Radio_1" name="Radio" checktype="c1" value="First&#x20;Choice">
                                        <label for="Radio_1" class="zf-radioChoice">First&#x20;Choice</label>
                                    </span>
                                    <span class="zf-multiAttType">
                                        <input class="zf-radioBtnType" type="radio" id="Radio_2" name="Radio" checktype="c1" value="Second&#x20;Choice">
                                        <label for="Radio_2" class="zf-radioChoice">Second&#x20;Choice</label>
                                    </span>
                                    <div class="zf-clearBoth"></div>
                                </div>
                                <p id="Radio_error" class="zf-errorMessage" style="display: none;">Invalid value</p>
                            </div>
                            <div class="zf-clearBoth"></div>
                        </li>
                        <!---------Radio Ends Here---------->
                    </ul>
                </div>
                <!---------template Container Starts Here---------->
                <ul>
                    <li class="zf-fmFooter">
                        <button class="zf-submitColor">Submit</button></li>
                </ul>
            </div>
            <!-- 'zf-templateWrapper' ends -->
        </form>
    </div>
    <!-- 'zf-templateWidth' ends -->
    <script type="text/javascript">var zf_DateRegex = new RegExp("^(([0][1-9])|([1-2][0-9])|([3][0-1]))[-](Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC)[-](?:(?:19|20)[0-9]{2})$");
        var zf_MandArray = [];
        var zf_FieldArray = ["Radio"];
        var isSalesIQIntegrationEnabled = false;
        var salesIQFieldsArray = [];</script>
</body>
</html>
