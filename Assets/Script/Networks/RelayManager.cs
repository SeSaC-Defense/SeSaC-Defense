using System;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayManager : MonoBehaviour
{
    [SerializeField]
    GameObject uiRelay;
    [SerializeField]
    GameObject panelLoading;
    [SerializeField]
    MessageBox messageBox;
    [SerializeField]
    UIGroupHost uiGroupHost;
    [SerializeField]
    UIGroupGuest uiGroupGuest;

    private Allocation allocation;
    private JoinAllocation joinAllocation;

    public async void OnStartHost()
    {
        uiGroupHost.SetStartButtonInteractable(false);
        panelLoading.SetActive(true);

        try
        {
            await SignInAsync();
            allocation = await RelayService.Instance.CreateAllocationAsync(1);

            var data = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(data);
            NetworkManager.Singleton.StartHost();

            uiGroupHost.SetStartButtonInteractable(true);
        }
        catch (Exception e)
        {
            ShowErrorMessage(e);
        }

        panelLoading.SetActive(false);
    }

    public async void OnStartGuest()
    {
        uiGroupGuest.SetStartButtonInteractable(false);
        panelLoading.SetActive(true);

        try
        {
            await SignInAsync();
            uiGroupGuest.SetStartButtonInteractable(true);
        }
        catch (Exception e)
        {
            ShowErrorMessage(e);
        }

        panelLoading.SetActive(false);
    }

    public async Task SignInAsync()
    {
        if (ServicesInitializationState.Initialized != UnityServices.State)
            await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void GetJoinCode()
    {
        panelLoading.SetActive(true);

        try
        {
            string joinCode = await GetJoinCodeAsync();
            uiGroupHost.SetJoinCodeText(joinCode);
        }
        catch (Exception e)
        {
            ShowErrorMessage(e);
        }

        panelLoading.SetActive(false);
    }

    public async Task<string> GetJoinCodeAsync()
    {
        return await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
    }

    public void OnStartGameHost()
    {
        try
        {

            uiRelay.SetActive(false);
        }
        catch (Exception e)
        {
            ShowErrorMessage(e);
        }
    }

    public async void OnStartGameGuest()
    {
        try
        {
            await SetJoinAllocation();
        }
        catch (Exception e)
        {
            ShowErrorMessage(e);
            panelLoading.SetActive(false);
            return;
        }

        try
        {
            var data = new RelayServerData(joinAllocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(data);
            NetworkManager.Singleton.StartClient();

            uiRelay.SetActive(false);
        }
        catch (Exception e)
        {
            ShowErrorMessage(e);
        }
    }

    public async Task SetJoinAllocation()
    {

        panelLoading.SetActive(true);

        string joinCode = uiGroupGuest.GetJoinCodeText();
        joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        panelLoading.SetActive(false);
    }

    public void ShowErrorMessage(Exception e)
    {
        Debug.LogError(e);
        ShowMessage(e.Message);
    }

    public void ShowMessage(string message)
    {
        messageBox.SetActive(true);
        messageBox.SetMessage(message);
    }
}
