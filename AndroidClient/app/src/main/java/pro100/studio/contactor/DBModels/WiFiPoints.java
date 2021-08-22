package pro100.studio.contactor.DBModels;

import com.orm.SugarRecord;

public class WiFiPoints extends SugarRecord{
    private String PSSD;
    private String Level;

    public WiFiPoints() {
    }

    public String getPSSD() {
        return PSSD;
    }

    public String getLevel() {
        return Level;
    }

    public WiFiPoints(String PSSD, String level) {
        this.PSSD = PSSD;
        Level = level;
    }
}
