<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="MyAccount" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlace1" runat="Server">


    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" Width="100%" Height="100%" runat="server" Theme="Metropolis" HeaderText="Account Management">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxFormLayout runat="server" ID="OptionsFormLayout" Theme="Metropolis">
                    <Items>
                        <dx:LayoutGroup Caption="Account Information">
                            <Items>
                                <dx:LayoutItem Caption="Full name">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txtFullName" ReadOnly="true" runat="server" AutoPostBack="false"></dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Old Passcode">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txtOldPassCode" runat="server" AutoPostBack="false"></dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="New Passcode">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txtNewPasscode" runat="server" AutoPostBack="false"></dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Confirm Passcode">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxTextBox ID="txtNewPasscodeConfirm" runat="server" AutoPostBack="false"></dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Language">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxComboBox ID="cbLanguage" runat="server" SelectedIndex="0" ValueType="System.Int32">
                                                <Items>
                                                    <dx:ListEditItem Selected="True" Text="Vietnamese" Value="0"></dx:ListEditItem>
                                                    <dx:ListEditItem Text="English" Value="1"></dx:ListEditItem>
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption=" ">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton runat="server" Text="Submit" ID="ASPxButton1" OnClick="ASPxButton1_Click"></dx:ASPxButton>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption=" ">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <asp:Label runat="server" ForeColor="Red" ID="lblError"></asp:Label>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>

            </dx:PanelContent>
        </PanelCollection>
        <BorderBottom BorderStyle="None" />
    </dx:ASPxRoundPanel>



</asp:Content>

