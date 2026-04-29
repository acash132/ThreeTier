using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ThreeTier_Arc
{
    public partial class LoginTier : System.Web.UI.Page
    {
        UserBLL bll = new UserBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        private void BindGrid(string search = "")
        {
            grdData.DataSource = bll.GetAllUsers(search);
            grdData.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bll.InsertUser(txtUsername.Text.Trim(), txtPassword.Text.Trim());
            txtUsername.Text = "";
            txtPassword.Text = "";
            BindGrid();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            // Use ViewState to store selected UID when a row is selected
            if (ViewState["SelectedUID"] != null)
            {
                int uid = Convert.ToInt32(ViewState["SelectedUID"]);
                bll.UpdateUser(uid, txtUsername.Text.Trim(), txtPassword.Text.Trim());
                ViewState["SelectedUID"] = null;
                BindGrid();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedUID"] != null)
            {
                int uid = Convert.ToInt32(ViewState["SelectedUID"]);
                bll.DeleteUser(uid);
                ViewState["SelectedUID"] = null;
                BindGrid();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid(txtSearch.Text.Trim());
        }

        // GridView inline edit handlers
        protected void grdData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdData.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void grdData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdData.EditIndex = -1;
            BindGrid();
        }

        protected void grdData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int uid = Convert.ToInt32(grdData.DataKeys[e.RowIndex].Value);

            // Find the textboxes inside the EditItemTemplate
            TextBox txtUser = (TextBox)grdData.Rows[e.RowIndex].FindControl("txtUsername");
            TextBox txtPass = (TextBox)grdData.Rows[e.RowIndex].FindControl("txtPassword");

            bll.UpdateUser(uid, txtUser.Text.Trim(), txtPass.Text.Trim());

            grdData.EditIndex = -1;
            BindGrid();
        }

        protected void grdData_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int uid = Convert.ToInt32(grdData.DataKeys[e.RowIndex].Value);
            bll.DeleteUser(uid);
            BindGrid();
        }

        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}