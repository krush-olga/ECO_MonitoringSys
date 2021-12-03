import React, { useEffect, useState } from 'react';
import { Popup, Marker } from 'react-leaflet';
import { divIcon } from 'leaflet/dist/leaflet-src.esm';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
  faPencilAlt,
  faBalanceScale,
  faPoll,
} from '@fortawesome/free-solid-svg-icons';

import '../popup.css';
import { RadiationResultsModal } from '../../radiationResultsModal/radiationResultsModal';

const RadiationPoint = ({
  id,
  coordinates,
  Name_object,
  description,
  image,
  owner_type,
  // handleClick,
}) => {
  const [modalShow, setModalShow] = useState(false);

  return (
    <Marker
      position={coordinates}
      icon={divIcon({
        html: `
          <div class="leaflet-div-icon-wraper">
            <img class="wraper_icon" src="${image}" />
            <svg class="svg_filler" viewBox="0 0 40 55" xmlns="http://www.w3.org/2000/svg">
              <path style="fill: grey; stroke: rgb(0, 0, 0); paint-order: stroke;" d="M 19.742 52.481 C 19.35 52.248 18.131 52.182 16.788 50.942 C 15.401 49.661 14.267 48.988 13.077 47.734 C 11.293 45.855 10.319 44.263 9.121 42.344 C 5.898 37.183 2.623 32.147 1.758 25.723 C 1.22 21.729 0.502 17.489 1.721 13.391 C 3.248 8.256 6.509 4.389 12.588 2.157 C 13.066 1.982 14.057 1.564 14.609 1.472 C 15.143 1.383 15.752 1.299 16.256 1.151 C 21.974 -0.523 27.779 0.952 34.097 6.63 C 37.688 9.857 39.355 15 39.127 20.994 C 38.933 26.087 39.102 31.087 35.405 36.007 C 37.668 32.5 25.365 54.955 19.716 52.365 C 18.622 51.863 30.639 29.002 29.861 27.599 C 29.864 27.599 30.627 26.058 31.425 23.619 C 32.146 21.415 33.086 17.55 30.789 13.627 C 29.632 11.652 27.689 9.557 25.593 8.408 C 23.982 7.525 22.01 7.32 20.447 7.505 C 18.226 7.768 16.089 8.043 14.533 8.742 C 13.397 9.252 11.812 10.129 10.421 11.136 C 9.579 11.746 9.003 13.211 8.631 14.522 C 8.32 15.616 8.256 16.698 8.122 17.938 C 7.787 21.042 8.564 24.565 10.941 27.718 C 12.408 29.664 15.125 30.955 17.804 31.57 C 21.789 32.485 25.982 31.036 29.742 27.499 C 32.037 25.34 35.748 33.857 35.748 33.857"/>
            </svg>
          </div>
          `,
      })}
    >
      <Popup
        maxWidth={window.innerWidth >= 991 ? 'auto' : window.innerWidth / 1.2}
      >
        {sessionStorage.getItem('user') && (
          <FontAwesomeIcon
            icon={faPoll}
            onClick={() => setModalShow(true)}
            className='poll-icon'
          />
        )}
        <div className='mt-4 mb-2'>
          {Name_object && (
            <div>
              <strong>Назва:</strong> {Name_object}
            </div>
          )}
          {description && (
            <div>
              <strong>Опис:</strong> {description}
            </div>
          )}
          {owner_type && (
            <div>
              <strong>Форма власності:</strong> {owner_type.name}
            </div>
          )}
        </div>
        <RadiationResultsModal
          id={id}
          show={modalShow}
          onHide={() => setModalShow(false)}
        />
      </Popup>
    </Marker>
  );
};

export const RadiationPoints = ({ radiationPoints }) => {
  return (
    <>
      {radiationPoints.map(
        ({
          Id: id,
          coordinates,
          Name_object,
          Description: description,
          Image: image,
          owner_type,
        }) => (
          <>
            <RadiationPoint
              id={id}
              key={id}
              coordinates={coordinates}
              Name_object={Name_object}
              description={description}
              image={image}
              owner_type={owner_type}
            />
          </>
        )
      )}
    </>
  );
};
