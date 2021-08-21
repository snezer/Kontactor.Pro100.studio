package pro100.studio.contactor.DBModels;

import com.orm.SugarRecord;

public class WiFiPoints extends SugarRecord<PointOnPlane> {
    private String PSSD;
    private String Level;

    public WiFiPoints() {
    }

    public WiFiPoints(String PSSD, String level) {
        this.PSSD = PSSD;
        Level = level;
    }
}
