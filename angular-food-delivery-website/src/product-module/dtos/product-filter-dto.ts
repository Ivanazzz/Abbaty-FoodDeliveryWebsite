export class ProductFilterDto {
  priceMin: number;
  priceMax: number;
  type: ProductType;
}

export enum ProductType {
  Salad = 1,
  Starter = 2,
  Main = 3,
  Seafood = 4,
  Bread = 5,
  Dessert = 6,
  Children = 7,
}

export const ProductTypeEnumLocalization = {
  [ProductType.Salad]: "Салата",
  [ProductType.Starter]: "Предястие",
  [ProductType.Main]: "Основно",
  [ProductType.Seafood]: "Морски дарове",
  [ProductType.Bread]: "Хляб",
  [ProductType.Dessert]: "Десерт",
  [ProductType.Children]: "Детско",
};
