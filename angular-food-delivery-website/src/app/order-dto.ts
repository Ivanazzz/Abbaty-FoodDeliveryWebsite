import { OrderItemDto } from "./order-item-dto";

export class OrderDto {
  orderItems: OrderItemDto[] = [];
  addressId: number;
  discountId: number;
  deliveryPrice: number;
  totalPrice: number;
}
