import { Component, OnInit } from '@angular/core';
import { ImagesService } from '../_services/images.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  images = [];

  imageModel = 'bs-items-seattle';

  constructor(private imageService: ImagesService) {}

  ngOnInit() {
    const imageUrls = [];
    this.imageService
      .getImages(this.imageModel)
      .pipe(
        map((response: any) => {
          const result = response;
          if (result) {
            result.forEach((s: { bucketName: string; key: string }) => {
              imageUrls.push(
                'https://' + s.bucketName + '.s3.amazonaws.com/' + s.key
              );
            });
          }
        })
      )
      .subscribe(
        next => {
          this.images = imageUrls;
        },
        error => {
          console.log(error);
        }
      );
  }
}
