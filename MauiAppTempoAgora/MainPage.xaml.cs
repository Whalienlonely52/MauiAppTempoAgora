using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = $"Latitude: {t.lat}\n" +
                                                $"Longitude: {t.lon}\n" +
                                                $"Nascer do Sol: {t.sunrise}\n" +
                                                $"Por do Sol: {t.sunset}\n" +
                                                $"Temp Máx: {t.temp_max}°C\n" +
                                                $"Temp Min: {t.temp_min}°C\n" +
                                                $"Clima: {t.main} - {t.description}\n" +
                                                $"Velocidade do Vento: {t.speed} m/s\n" +
                                                $"Visibilidade: {t.visibility} m";

                        lbl_res.Text = dados_previsao;
                    }
                    else
                    {
                        await DisplayAlert("Cidade não encontrada", "Não foi possível encontrar cidade informada.", "OK");
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Sem conexão", "Verifique sua conexão com a internet.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}