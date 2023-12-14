var sizeManager = (function () {
    var defaultSizes = {
        toolbarHeight: 44,
        SidebarWidth: 300,
        SidebarWidthWithotBorders: 299,
        tabBarHeight: 49,
        contentHorizontalMargins: 60,
        contentVerticalMargins: 60
    }
    var layout;
    function GetEmptySpace() {
        return { width: ASPxClientUtils.GetDocumentClientWidth(), height: ASPxClientUtils.GetDocumentClientHeight() };
    }
    function GetHeightWithoutFooter() {
        return GetEmptySpace().height - (layout.footer ? defaultSizes.tabBarHeight : 0);
    }
    function GetWidthWithoutSidebar() {
        return GetEmptySpace().width - (layout.sideContentPanel && IsSidebarVisible() ? defaultSizes.SidebarWidth : 0);
    }
    function GetSidebarHeight() {
        return GetHeightWithoutFooter();
    }
    function GetContentSize() {
        return { width: GetWidthWithoutSidebar(), height: GetHeightWithoutFooter() };
    }
    function IsSidebarVisible() {
        return ASPxClientTouchUI.getIsLandscapeOrientation();
    }
    function AdjustSizesCore() {
        var emptySpace = GetEmptySpace();
        var contentSize = GetContentSize();
        var sideBarHeight = GetSidebarHeight();

        if(layout.footer) {
            layout.footer.SetWidth(emptySpace.width);
            layout.footer.SetHeight(defaultSizes.tabBarHeight);
        }
        if(layout.sidePanel && IsSidebarVisible()){
            layout.sidePanel.SetWidth(defaultSizes.SidebarWidthWithotBorders);
            layout.sidePanel.SetHeight(sideBarHeight);
            if(layout.sideContentPanel){
                layout.sideContentPanel.SetWidth(defaultSizes.SidebarWidthWithotBorders);
                layout.sideContentPanel.SetHeight(sideBarHeight - (layout.sideToolbarExists ? defaultSizes.toolbarHeight : 0));
            }
            if(layout.sideControl){
                layout.sideControl.SetWidth(defaultSizes.SidebarWidthWithotBorders);
                layout.sideControl.SetHeight(sideBarHeight);
            }
        }
        if(layout.MainCallbackPanel) {
            layout.MainCallbackPanel.SetWidth(emptySpace.width);
            layout.MainCallbackPanel.SetHeight(emptySpace.height - (layout.footer ? defaultSizes.tabBarHeight : 0));
        }
        if(layout.contentCallbackPanel)
        {
            layout.contentCallbackPanel.SetWidth(contentSize.width);
            layout.contentCallbackPanel.SetHeight(contentSize.height);
        }
        if(layout.contentPanel){
            layout.contentPanel.SetWidth(contentSize.width);
            layout.contentPanel.SetHeight(contentSize.height - (layout.contentToolbarExists ? defaultSizes.toolbarHeight : 0));
        }
        if(layout.content){
            layout.content.SetWidth(contentSize.width - defaultSizes.contentHorizontalMargins);
            // Uncomment the next line if the content control height should be calculated
            // layout.content.SetHeight(contentSize.height - (layout.contentToolbarExists ? defaultSizes.toolbarHeight : 0) - defaultSizes.contentVerticalMargins);
        }
        AdjustSizesCustom();
    }
    function AdjustSizesCustom(){
        // Put custom resizing code here
    }
    return {
        Init: function(currentLayout) {
            layout = currentLayout;
            var eventName = ASPxClientUtils.iOSPlatform ? "orientationchange" : "resize";
            ASPxClientUtils.AttachEventToElement(window, eventName, function () { sizeManager.AdjustSizes(); }, false);
            AdjustSizesCore();
        },
        EndCallback: function() {
            AdjustSizesCore();
        },
        AdjustSizes: function(){
            AdjustSizesCore();
        }
    }
})();