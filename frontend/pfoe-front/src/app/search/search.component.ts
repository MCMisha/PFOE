import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {EventModel} from "../models/eventModel";
import {SearchService} from "../services/search.service";
import {ActivatedRoute} from "@angular/router";
import {MatPaginator} from "@angular/material/paginator";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})

export class SearchComponent implements OnInit{
  dataSource: MatTableDataSource<EventModel> = new MatTableDataSource<EventModel>();
  events: EventModel[] = [];
  columnsToDisplay = ['name', 'location', 'category', 'date', 'participantNumber'];
  @ViewChild(MatPaginator) paginator!: MatPaginator

  constructor(private route:ActivatedRoute, private searchService : SearchService) {
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const query = params['q'];

      this.events = []

      if(query) {
        this.search(query);
        this.dataSource.paginator = this.paginator;
      }
    })
  }

  search(query: string){
    this.events = []
    this.searchService.search(query).subscribe(res => {
        if(res  != null) {
          this.events = res as EventModel[];
          this.dataSource.data = this.events;
          console.log(this.events);
        }
      }
    )
  }
}
