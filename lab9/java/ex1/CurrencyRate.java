package ex1;

public class CurrencyRate {
    private String currencyCode;
    private double buyRate;
    private double sellRate;
    private String lastUpdated;

    public CurrencyRate(String currencyCode, double buyRate, double sellRate, String lastUpdated) {
        this.currencyCode = currencyCode;
        this.buyRate = buyRate;
        this.sellRate = sellRate;
        this.lastUpdated = lastUpdated;
    }
    public String getCurrencyCode() {
        return currencyCode;
    }
    public double getBuyRate() {
        return buyRate;
    }
    public double getSellRate() {
        return sellRate;
    }
    public String getLastUpdated() {
        return lastUpdated;
    }
    public void setBuyRate(double buyRate) {
        this.buyRate = buyRate;
    }
    public void setSellRate(double sellRate) {
        this.sellRate = sellRate;
    }
    public void setLastUpdated(String lastUpdated) {
        this.lastUpdated = lastUpdated;
    }
    @Override
    public String toString() {
        return ( currencyCode +" Купівля " + buyRate + " / Продаж " + sellRate +" Час: "+ lastUpdated);
    }
}
