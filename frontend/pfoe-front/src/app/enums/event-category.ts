export enum EventCategory {
  BIZNES = 'Biznes',
  EDUKACJA = 'Edukacja',
  ROZRYWKA = 'Rozrywka',
  SPORT = 'Sport',
  MUZYKA = 'Muzyka'
}

export const eventCategoriesArray = Object.keys(EventCategory).map(key => EventCategory[key as keyof typeof EventCategory]);
