﻿using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Services.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Shared.Forms.Abstract
{
    public abstract class AccountForm : ComponentBase
    {
        [CascadingParameter]
        protected MudDialogInstance MudDialog { get; set; }

        protected bool _isValidationSuccessful;
        protected User User { get; set; } = new User();

        [Inject]
        protected IAccountService Account { get; set; }
        [Inject]
        protected IDialogService DialogService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected bool IsOverlayVisible { get; set; }

        protected abstract Task SignOperation();

        protected async void HandleUserSubmit()
        {
            await SignOperation();

            if (!Account.IsAuthentificated)
            {
                await DialogService.ShowMessageBox(
                    "Error", "The password or email is incorrect");
                return;
            }

            User = new User();
            MudDialog.Close();
            NavigationManager.NavigateTo("/report");
        }

        protected void ShowOverlay()
        {
            IsOverlayVisible = true;
            StateHasChanged();
        }

        protected void HideOverlay()
        {
            IsOverlayVisible = false;
            StateHasChanged();
        }
    }
}
