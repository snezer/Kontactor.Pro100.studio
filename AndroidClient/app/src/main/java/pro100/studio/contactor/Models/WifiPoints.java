package pro100.studio.contactor.Models;

public class WifiPoints {
    private String ssid;
    private Double level;

    public String getSsid() {
        return ssid;
    }

    public void setSsid(String ssid) {
        this.ssid = ssid;
    }

    public WifiPoints(String ssid, Double level) {
        this.ssid = ssid;
        this.level = level;
    }
}
