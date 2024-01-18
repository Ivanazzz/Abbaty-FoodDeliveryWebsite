export class ProductDto {
    id: number
    name: string
    description: string
    price: number
    grams: number
    type: ProductType
    status: ProductStatus
    image: File
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

  export enum ProductStatus {
    Available = 1,
    Unavailable = 2
  }
  
  export const ProductTypeEnumLocalization = {
    [ProductType.Salad]: 'Салата',
    [ProductType.Starter]: 'Предястие',
    [ProductType.Main]: 'Основно',
    [ProductType.Seafood]: 'Морски дарове',
    [ProductType.Bread]: 'Хляб',
    [ProductType.Dessert]: 'Десерт',
    [ProductType.Children]: 'Детско'
  };