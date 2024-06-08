export class EventModel {
  id?: number = 0;
  name?: string | null | undefined;
  category?: string | null | undefined;
  date?: string | null | undefined;
  participantNumber?: number | null | undefined;
  location?: string | null | undefined;
  organizer?: number | null | undefined;
  visitsNumber?: number | null | undefined;
  creationDate?: Date | null | undefined;
}
