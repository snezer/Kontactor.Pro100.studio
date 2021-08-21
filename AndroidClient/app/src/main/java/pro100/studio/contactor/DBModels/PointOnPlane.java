package pro100.studio.contactor.DBModels;

import com.orm.SugarRecord;

import java.util.List;

public class PointOnPlane extends SugarRecord<PointOnPlane> {
    public String getFloorName() {
        return FloorName;
    }

    public String getCoordMPoint() {
        return CoordMPoint;
    }

    public String getCoordLPoint() {
        return CoordLPoint;
    }

    public List<WiFiPoints> getWiFiPointList() {
        return WiFiPointList;
    }

    private String FloorName;
    private String CoordMPoint;
    private String CoordLPoint;
    private List<WiFiPoints> WiFiPointList;

    public PointOnPlane(String floorName, String coordMPoint, String coordLPoint, List<WiFiPoints> wiFiPointList) {
        FloorName = floorName;
        CoordMPoint = coordMPoint;
        CoordLPoint = coordLPoint;
        WiFiPointList = wiFiPointList;
    }

    public PointOnPlane() {
    }
}
