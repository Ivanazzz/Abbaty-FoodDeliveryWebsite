import { ProductOrderDto } from "./product-order-dto";

export class OrderItemDto {
  id: number;
  product: ProductOrderDto;
  price: number;
  productQuantity: number;
}
