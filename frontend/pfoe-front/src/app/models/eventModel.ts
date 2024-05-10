export class EventModel {
  id?: number = 0;
  name?: string | null | undefined;
  location?: string | null | undefined;
  category?: string | null | undefined;
  date?: string | null | undefined;
  participantNumber?: number | null | undefined;
  organizer?: number | null | undefined;
  visits_number?: number | null | undefined;
  creation_date?: Date | null | undefined;
}
