package Task2.documents;

import Task2.channels.DeliveryChannel;

public abstract class PaymentDocument{
   protected DeliveryChannel channel;
    public PaymentDocument(DeliveryChannel channel) {
        this.channel = channel;
    }
    public abstract void generate();
}
